import { Component } from '@angular/core';
import { SettingsService } from '../../services/settings/settings.service';
import { SettingsResponse } from '../../services/settings/models/settings-response.interface';
import { SettingsRequest } from '../../services/settings/models/settings-request.interface';
import { NgIcon, NgIconComponent } from '@ng-icons/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-settings',
  imports: [NgIcon, NgIconComponent, ReactiveFormsModule],
  templateUrl: './settings.html',
  styleUrl: './settings.css',
})
export class Settings {
  settings: SettingsResponse | undefined;
  isSubmitting: boolean = false;

  formGroup = new FormGroup({
    platformMirrorCron: new FormControl('', [Validators.required]),
    repositoryMirrorCron: new FormControl('', [Validators.required])
  });

  constructor(private settingService: SettingsService) { }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.settings = undefined;
    this.settingService.get().subscribe({
      next: (response) => {
        this.settings = response;
        this.formGroup.setValue(response);
      }
    });
  }

  saveSettings() {
    if (!this.formGroup.valid) {
      this.formGroup.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    this.settingService.update(this.formGroup.value as SettingsRequest).subscribe({
      next: (response) => {
        this.settings = response;
        this.isSubmitting = false;
      },
      error: () => {
        this.isSubmitting = false;
      }
    });
  }

  triggerJob(jobName: string) {
    this.settingService.triggerJob(jobName).subscribe({
      next: () => {
        
      }
    });
  }

  openHangfire() {
    window.open('/hangfire', '_blank');
  }
}
