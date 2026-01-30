import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PlatformService } from '../../../services/platform/platform.service';
import { PlatformRequest } from '../../../services/platform/models/platform-request.interface';
import { PlatformType } from '../../../services/platform/models/platform-type.enum';
import { Loader } from '../../../components/loader/loader';
import { NgIf, NgFor } from '@angular/common';
import { GenericOffCanvasDrawer } from "../../../components/generic-off-canvas-drawer/generic-off-canvas-drawer";

@Component({
  selector: 'app-create-platform',
  imports: [ReactiveFormsModule, GenericOffCanvasDrawer, Loader, NgIf, NgFor],
  templateUrl: './create-platform.html',
  styleUrl: './create-platform.css',
})
export class CreatePlatform {
  @ViewChild(GenericOffCanvasDrawer) drawer!: GenericOffCanvasDrawer;
  @Output() drawerClosed = new EventEmitter();

  formGroup = new FormGroup({
    type: new FormControl<number>(PlatformType.AzureDevOps, [Validators.required]),
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    baseUrl: new FormControl('', [Validators.required])
  });

  platformTypes = [
    { value: PlatformType.AzureDevOps, label: 'Azure DevOps' },
    { value: PlatformType.GitLab, label: 'GitLab' },
    { value: PlatformType.GitHub, label: 'GitHub' },
    { value: PlatformType.Bitbucket, label: 'Bitbucket' }
  ];

  isSubmitting: boolean = false;

  constructor(private platformService: PlatformService, private router: Router) { }

  createPlatform() {
    if (!this.formGroup.valid) {
      this.formGroup.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    this.platformService.create(this.formGroup.value as PlatformRequest).subscribe({
      next: x => {
        this.drawerClosed.emit();
        this.formGroup.reset({ type: PlatformType.GitHub });
        this.drawer.closeDrawer();
        this.isSubmitting = false;
      },
      error: () => {
        this.isSubmitting = false;
      }
    });
  }
}
