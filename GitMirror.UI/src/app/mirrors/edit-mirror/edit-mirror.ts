import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { GenericOffCanvasDrawer } from '../../../components/generic-off-canvas-drawer/generic-off-canvas-drawer';
import { MirrorService } from '../../../services/mirror/mirror.service';
import { MirrorRequest } from '../../../services/mirror/models/mirror-request.interface';
import { MirrorResponse } from '../../../services/mirror/models/mirror-response.interface';
import { PlatformResponse } from '../../../services/platform/models/platform-response.interface';
import { PlatformType } from '../../../services/platform/models/platform-type.enum';
import { Loader } from '../../../components/loader/loader';
import { NgIf, NgFor } from '@angular/common';

@Component({
  selector: 'app-edit-mirror',
  imports: [GenericOffCanvasDrawer, ReactiveFormsModule, Loader, NgIf, NgFor],
  templateUrl: './edit-mirror.html',
  styleUrl: './edit-mirror.css',
})
export class EditMirror {
  @ViewChild(GenericOffCanvasDrawer) drawer!: GenericOffCanvasDrawer;
  @Output() drawerClosed = new EventEmitter();
  @Input() mirror: MirrorResponse | undefined;
  @Input() platforms: PlatformResponse[] = [];

  formGroup = new FormGroup({
    sourcePlatformId: new FormControl('', [Validators.required]),
    targetPlatformId: new FormControl('', [Validators.required])
  });

  isSubmitting: boolean = false;

  constructor(private mirrorService: MirrorService) { }

  openDrawer(mirror: MirrorResponse) {
    this.mirror = mirror;
    this.formGroup.patchValue({
      sourcePlatformId: mirror.sourcePlatformId,
      targetPlatformId: mirror.targetPlatformId
    });
  }

  updateMirror() {
    if (!this.formGroup.valid || !this.mirror) {
      this.formGroup.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    this.mirrorService.update(this.mirror.id, this.formGroup.value as MirrorRequest).subscribe({
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
