import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { GenericOffCanvasDrawer } from '../../../components/generic-off-canvas-drawer/generic-off-canvas-drawer';
import { MirrorService } from '../../../services/mirror/mirror.service';
import { MirrorRequest } from '../../../services/mirror/models/mirror-request.interface';
import { PlatformResponse } from '../../../services/platform/models/platform-response.interface';
import { PlatformType } from '../../../services/platform/models/platform-type.enum';
import { Loader } from '../../../components/loader/loader';
import { NgIf, NgFor } from '@angular/common';

@Component({
  selector: 'app-create-mirror',
  imports: [GenericOffCanvasDrawer, ReactiveFormsModule, Loader, NgIf, NgFor],
  templateUrl: './create-mirror.html',
  styleUrl: './create-mirror.css',
})
export class CreateMirror {
  @ViewChild(GenericOffCanvasDrawer) drawer!: GenericOffCanvasDrawer;
  @Output() drawerClosed = new EventEmitter();
  @Input() platforms: PlatformResponse[] = [];

  formGroup = new FormGroup({
    sourcePlatformId: new FormControl('', [Validators.required]),
    targetPlatformId: new FormControl('', [Validators.required])
  });

  isSubmitting: boolean = false;

  constructor(private mirrorService: MirrorService) { }

  createMirror() {
    if (!this.formGroup.valid) {
      this.formGroup.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    this.mirrorService.create(this.formGroup.value as MirrorRequest).subscribe({
      next: () => {
        this.drawerClosed.emit();
        this.formGroup.reset();
        this.drawer.closeDrawer();
        this.isSubmitting = false;
      },
      error: () => {
        this.isSubmitting = false;
      }
    });
  }

  getPlatformLabel(type: PlatformType): string {
    switch (type) {
      case PlatformType.GitHub:
        return 'GitHub';
      case PlatformType.GitLab:
        return 'GitLab';
      case PlatformType.AzureDevOps:
        return 'Azure DevOps';
      case PlatformType.Bitbucket:
        return 'Bitbucket';
      default:
        return 'Unknown';
    }
  }
}
