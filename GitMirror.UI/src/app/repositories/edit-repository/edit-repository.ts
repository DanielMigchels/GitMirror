import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { GenericOffCanvasDrawer } from '../../../components/generic-off-canvas-drawer/generic-off-canvas-drawer';
import { RepositoryService } from '../../../services/repository/history.service';
import { RepositoryRequest } from '../../../services/repository/models/repository-request.interface';
import { RepositoryResponse } from '../../../services/repository/models/repository-response.interface';
import { Loader } from '../../../components/loader/loader';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-edit-repository',
  imports: [GenericOffCanvasDrawer, ReactiveFormsModule, Loader, NgIf],
  templateUrl: './edit-repository.html',
  styleUrl: './edit-repository.css',
})
export class EditRepository {
  @ViewChild(GenericOffCanvasDrawer) drawer!: GenericOffCanvasDrawer;
  @Output() drawerClosed = new EventEmitter();
  @Input() repository: RepositoryResponse | undefined;

  formGroup = new FormGroup({
    sourceCloneUrl: new FormControl('', [Validators.required]),
    sourceUsername: new FormControl('', [Validators.required]),
    sourcePassword: new FormControl(''),
    targetCloneUrl: new FormControl('', [Validators.required]),
    targetUsername: new FormControl('', [Validators.required]),
    targetPassword: new FormControl('')
  });

  isSubmitting: boolean = false;

  constructor(private repositoryService: RepositoryService) { }

  openDrawer(repository: RepositoryResponse) {
    this.repository = repository;
    this.formGroup.patchValue({
      sourceCloneUrl: repository.sourceCloneUrl,
      sourceUsername: repository.sourceUsername,
      sourcePassword: '',
      targetCloneUrl: repository.targetCloneUrl,
      targetUsername: repository.targetUsername,
      targetPassword: ''
    });
  }

  updateRepository() {
    if (!this.formGroup.valid || !this.repository) {
      this.formGroup.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    this.repositoryService.update(this.repository.id, this.formGroup.value as RepositoryRequest).subscribe({
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
}
