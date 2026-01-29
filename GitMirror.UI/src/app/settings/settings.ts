import { Component } from '@angular/core';
import { SettingsService } from '../../services/settings/settings.service';
import { SettingsResponse } from '../../services/settings/models/settings-response.interface';

@Component({
  selector: 'app-settings',
  imports: [],
  templateUrl: './settings.html',
  styleUrl: './settings.css',
})
export class Settings {
  settings: SettingsResponse | undefined;

  constructor(private settingService: SettingsService) { }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.settings = undefined;
    this.settingService.get().subscribe({
      next: (response) => {
        this.settings = response;
      }
    });
  }
}
