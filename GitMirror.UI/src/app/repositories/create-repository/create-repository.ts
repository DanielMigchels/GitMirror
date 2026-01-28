import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { GenericOffCanvasDrawer } from '../../../components/generic-off-canvas-drawer/generic-off-canvas-drawer';
import { RepositoryService } from '../../../services/repository/history.service';
import { RepositoryRequest } from '../../../services/repository/models/repository-request.interface';
import { Loader } from '../../../components/loader/loader';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-create-repository',
  imports: [GenericOffCanvasDrawer, ReactiveFormsModule, Loader, NgIf],
  templateUrl: './create-repository.html',
  styleUrl: './create-repository.css',
})
export class CreateRepository {
  @ViewChild(GenericOffCanvasDrawer) drawer!: GenericOffCanvasDrawer;
  @Output() drawerClosed = new EventEmitter();

  formGroup = new FormGroup({
    sourceCloneUrl: new FormControl('', [Validators.required]),
    sourceUsername: new FormControl(''),
    sourcePassword: new FormControl(''),
    targetCloneUrl: new FormControl('', [Validators.required]),
    targetUsername: new FormControl(''),
    targetPassword: new FormControl('')
  });

  isSubmitting: boolean = false;

  constructor(private repositoryService: RepositoryService) { }

  createRepository() {
    if (!this.formGroup.valid) {
      this.formGroup.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    this.repositoryService.create(this.formGroup.value as RepositoryRequest).subscribe({
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

