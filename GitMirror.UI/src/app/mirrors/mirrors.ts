import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Route, RouterLink } from '@angular/router';
import { NgIf, NgFor } from '@angular/common';
import { GenericBanner } from '../../components/generic-banner/generic-banner';
import { PlatformService } from '../../services/platform/platform.service';
import { MirrorService } from '../../services/mirror/mirror.service';
import { PaginatedList } from '../../services/pagination/paginated-list.interface';
import { PlatformResponse } from '../../services/platform/models/platform-response.interface';
import { MirrorResponse } from '../../services/mirror/models/mirror-response.interface';
import { PlatformType } from '../../services/platform/models/platform-type.enum';
import { NgIcon, NgIconComponent } from "@ng-icons/core";
import { CreateMirror } from './create-mirror/create-mirror';
import { EditMirror } from './edit-mirror/edit-mirror';
import { DeleteMirror } from './delete-mirror/delete-mirror';

@Component({
  selector: 'app-mirrors',
  imports: [RouterLink, GenericBanner, NgIf, NgFor, NgIcon, NgIconComponent, CreateMirror, EditMirror, DeleteMirror],
  templateUrl: './mirrors.html',
  styleUrl: './mirrors.css',
})
export class Mirrors implements OnInit {
  platforms: PaginatedList<PlatformResponse> | undefined;

  mirrors: PaginatedList<MirrorResponse> | undefined;
  selectedMirror: MirrorResponse | undefined;
  routeMirrorId: string | undefined;

  pageSize = 2147483647;
  page = 0;

  @ViewChild(CreateMirror) addMirrorDrawer!: CreateMirror;
  @ViewChild(EditMirror) editMirrorDrawer!: EditMirror;
  @ViewChild(DeleteMirror) deleteMirrorDrawer!: DeleteMirror;

  constructor(
    private route: ActivatedRoute,
    private platformService: PlatformService,
    private mirrorService: MirrorService
  ) {
    this.routeMirrorId = this.route.snapshot.paramMap.get('id')!;
  }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.platformService.get(2, 0).subscribe({
      next: (x: PaginatedList<PlatformResponse>) => this.platforms = x
    });

    this.mirrors = undefined;
    this.mirrorService.get(this.pageSize, this.page).subscribe({
      next: (x: PaginatedList<MirrorResponse>) => {
        this.mirrors = x

        if (this.routeMirrorId == undefined) {
          return;
        }

        const selectedMirror = this.mirrors.data.find(m => m.id === this.routeMirrorId);
          this.editMirror(new Event('click'), selectedMirror!);
      }
    });
  }

  addMirror(event: Event) {
    event.stopPropagation();
    this.addMirrorDrawer.drawer.openDrawer(event);
  }

  editMirror(event: Event, mirror: MirrorResponse) {
    event.stopPropagation();
    this.selectedMirror = mirror;
    this.editMirrorDrawer.openDrawer(mirror);
    this.editMirrorDrawer.drawer.openDrawer(event);
  }

  deleteMirror(event: Event, mirror: MirrorResponse) {
    event.stopPropagation();
    this.selectedMirror = mirror;
    this.deleteMirrorDrawer.openDrawer(mirror);
    this.deleteMirrorDrawer.drawer.openDrawer(event);
  }

  previousPage() {
    this.page--;
    this.fetchData();
  }

  nextPage() {
    this.page++;
    this.fetchData();
  }

  getPlatform(platformId: string): PlatformResponse | undefined {
    return this.platforms?.data.find(p => p.id === platformId);
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
