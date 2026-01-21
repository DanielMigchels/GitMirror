import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { GenericOffCanvasDrawer } from '../../../components/generic-off-canvas-drawer/generic-off-canvas-drawer';
import { PlatformService } from '../../../services/platform/platform.service';
import { PlatformResponse } from '../../../services/platform/models/platform-response.interface';
import { NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Loader } from '../../../components/loader/loader';

@Component({
  selector: 'app-delete-platform',
  imports: [GenericOffCanvasDrawer, NgIf, FormsModule, Loader],
  templateUrl: './delete-platform.html',
  styleUrl: './delete-platform.css',
})
export class DeletePlatform {
  @ViewChild(GenericOffCanvasDrawer) drawer!: GenericOffCanvasDrawer;
  @Output() drawerClosed = new EventEmitter();
  @Input() platform: PlatformResponse | undefined;
  
  isChecked: boolean = false;
  isSubmitting: boolean = false;

  constructor(private platformService: PlatformService) { }

  openDrawer(platform: PlatformResponse) {
    this.platform = platform;
    this.isChecked = false;
  }

  deletePlatform() {
    if (!this.platform || !this.isChecked) {
      return;
    }

    this.isSubmitting = true;

    this.platformService.delete(this.platform.id).subscribe({
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
