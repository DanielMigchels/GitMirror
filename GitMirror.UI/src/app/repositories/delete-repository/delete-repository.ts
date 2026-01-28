import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { GenericOffCanvasDrawer } from '../../../components/generic-off-canvas-drawer/generic-off-canvas-drawer';
import { RepositoryService } from '../../../services/repository/history.service';
import { RepositoryResponse } from '../../../services/repository/models/repository-response.interface';
import { NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Loader } from '../../../components/loader/loader';
import { NgIcon } from '@ng-icons/core';

@Component({
  selector: 'app-delete-repository',
  imports: [GenericOffCanvasDrawer, NgIf, FormsModule, Loader, NgIcon],
  templateUrl: './delete-repository.html',
  styleUrl: './delete-repository.css',
})
export class DeleteRepository {
  @ViewChild(GenericOffCanvasDrawer) drawer!: GenericOffCanvasDrawer;
  @Output() drawerClosed = new EventEmitter();
  @Input() repository: RepositoryResponse | undefined;
  
  isChecked: boolean = false;
  isSubmitting: boolean = false;

  constructor(private repositoryService: RepositoryService) { }

  openDrawer(repository: RepositoryResponse) {
    this.repository = repository;
    this.isChecked = false;
  }

  deleteRepository() {
    if (!this.repository || !this.isChecked) {
      return;
    }

    this.isSubmitting = true;

    this.repositoryService.delete(this.repository.id).subscribe({
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
