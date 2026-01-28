import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { GenericOffCanvasDrawer } from '../../../components/generic-off-canvas-drawer/generic-off-canvas-drawer';
import { MirrorService } from '../../../services/mirror/mirror.service';
import { MirrorResponse } from '../../../services/mirror/models/mirror-response.interface';
import { PlatformResponse } from '../../../services/platform/models/platform-response.interface';
import { NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Loader } from '../../../components/loader/loader';

@Component({
  selector: 'app-delete-mirror',
  imports: [GenericOffCanvasDrawer, NgIf, FormsModule, Loader],
  templateUrl: './delete-mirror.html',
  styleUrl: './delete-mirror.css',
})
export class DeleteMirror {
  @ViewChild(GenericOffCanvasDrawer) drawer!: GenericOffCanvasDrawer;
  @Output() drawerClosed = new EventEmitter();
  @Input() mirror: MirrorResponse | undefined;
  @Input() platforms: PlatformResponse[] = [];
  
  isChecked: boolean = false;
  isSubmitting: boolean = false;

  constructor(private mirrorService: MirrorService) { }

  openDrawer(mirror: MirrorResponse) {
    this.mirror = mirror;
    this.isChecked = false;
  }

  getPlatformUrl(platformId: string): string {
    const platform = this.platforms.find(p => p.id === platformId);
    return platform ? platform.baseUrl : 'Unknown';
  }

  deleteMirror() {
    if (!this.mirror || !this.isChecked) {
      return;
    }

    this.isSubmitting = true;

    this.mirrorService.delete(this.mirror.id).subscribe({
      next: () => {
        this.drawerClosed.emit();
        this.drawer.closeDrawer();
        this.isSubmitting = false;
        this.isChecked = false;
      },
      error: () => {
        this.isSubmitting = false;
      }
    });
  }
}
