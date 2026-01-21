import { Component, OnInit, ViewChild } from '@angular/core';
import { NgIconComponent } from '@ng-icons/core';
import { GenericBanner } from '../../components/generic-banner/generic-banner';
import { CreatePlatform } from "./create-platform/create-platform";
import { EditPlatform } from './edit-platform/edit-platform';
import { DeletePlatform } from './delete-platform/delete-platform';
import { PlatformResponse } from '../../services/platform/models/platform-response.interface';
import { PlatformType } from '../../services/platform/models/platform-type.enum';
import { PaginatedList } from '../../services/pagination/paginated-list.interface';
import { PlatformService } from '../../services/platform/platform.service';
import { MirrorService } from '../../services/mirror/mirror.service';
import { MirrorResponse } from '../../services/mirror/models/mirror-response.interface';
import { NgIf, NgFor } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-platforms',
  imports: [NgIconComponent, GenericBanner, CreatePlatform, EditPlatform, DeletePlatform, NgIf, NgFor],
  templateUrl: './platforms.html',
  styleUrl: './platforms.css',
})
export class Platforms implements OnInit{

  platforms: PaginatedList<PlatformResponse> | undefined;
  mirrors: PaginatedList<MirrorResponse> | undefined;
  selectedPlatform: PlatformResponse | undefined;

  constructor(private platformService: PlatformService, private mirrorService: MirrorService) { }

  pageSize = 2147483647;
  page = 0;

  @ViewChild(CreatePlatform) addPlatformDrawer!: CreatePlatform;
  @ViewChild(EditPlatform) editPlatformDrawer!: EditPlatform;
  @ViewChild(DeletePlatform) deletePlatformDrawer!: DeletePlatform;

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.platforms = undefined;
    this.platformService.get(this.pageSize, this.page).subscribe({
      next: (x: PaginatedList<PlatformResponse>) => this.platforms = x
    });
    
    this.mirrors = undefined;
    this.mirrorService.get(1, 0).subscribe({
      next: (x: PaginatedList<MirrorResponse>) => this.mirrors = x
    });
  }

  addPlatform(event: Event) {
    event.stopPropagation();
    this.addPlatformDrawer.drawer.openDrawer(event);
  }
  
  editPlatform(event: Event, platform: PlatformResponse) {
    event.stopPropagation();
    this.selectedPlatform = platform;
    this.editPlatformDrawer.openDrawer(platform);
    this.editPlatformDrawer.drawer.openDrawer(event);
  }

  deletePlatform(event: Event, platform: PlatformResponse) {
    event.stopPropagation();
    this.selectedPlatform = platform;
    this.deletePlatformDrawer.openDrawer(platform);
    this.deletePlatformDrawer.drawer.openDrawer(event);
  }

  previousPage() {
    this.page--;
    this.fetchData();
  }

  nextPage() {
    this.page++;
    this.fetchData();
  }

  getPlatformIcon(type: PlatformType): string {
    switch (type) {
      case PlatformType.GitHub:
        return 'simpleGithub';
      case PlatformType.GitLab:
        return 'simpleGitlab';
      case PlatformType.AzureDevOps:
        return 'heroCodeBracket';
      case PlatformType.Bitbucket:
        return 'simpleBitbucket';
      default:
        return 'heroCodeBracket';
    }
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
