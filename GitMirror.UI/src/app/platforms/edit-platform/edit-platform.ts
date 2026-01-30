import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { GenericOffCanvasDrawer } from '../../../components/generic-off-canvas-drawer/generic-off-canvas-drawer';
import { PlatformService } from '../../../services/platform/platform.service';
import { PlatformRequest } from '../../../services/platform/models/platform-request.interface';
import { PlatformResponse } from '../../../services/platform/models/platform-response.interface';
import { PlatformType } from '../../../services/platform/models/platform-type.enum';
import { Loader } from '../../../components/loader/loader';
import { NgIf, NgFor } from '@angular/common';

@Component({
  selector: 'app-edit-platform',
  imports: [GenericOffCanvasDrawer, ReactiveFormsModule, Loader, NgIf, NgFor],
  templateUrl: './edit-platform.html',
  styleUrl: './edit-platform.css',
})
export class EditPlatform {
  @ViewChild(GenericOffCanvasDrawer) drawer!: GenericOffCanvasDrawer;
  @Output() drawerClosed = new EventEmitter();
  @Input() platform: PlatformResponse | undefined;

  formGroup = new FormGroup({
    type: new FormControl<PlatformType>(PlatformType.AzureDevOps, [Validators.required]),
    username: new FormControl('', [Validators.required]),
    password: new FormControl(''),
    baseUrl: new FormControl('', [Validators.required])
  });

  platformTypes = [
    { value: PlatformType.AzureDevOps, label: 'Azure DevOps' },
    { value: PlatformType.GitLab, label: 'GitLab' },
    { value: PlatformType.GitHub, label: 'GitHub' },
    { value: PlatformType.Bitbucket, label: 'Bitbucket' }
  ];

  isSubmitting: boolean = false;

  constructor(private platformService: PlatformService) { }

  openDrawer(platform: PlatformResponse) {
    this.platform = platform;
    this.formGroup.patchValue({
      type: platform.type,
      username: platform.username,
      password: '',
      baseUrl: platform.baseUrl
    });
  }

  updatePlatform() {
    if (!this.formGroup.valid || !this.platform) {
      this.formGroup.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    this.platformService.update(this.platform.id, this.formGroup.value as PlatformRequest).subscribe({
      next: () => {
        this.drawerClosed.emit();
        this.formGroup.reset({ type: PlatformType.AzureDevOps });
        this.drawer.closeDrawer();
        this.isSubmitting = false;
      },
      error: () => {
        this.isSubmitting = false;
      }
    });
  }
}
