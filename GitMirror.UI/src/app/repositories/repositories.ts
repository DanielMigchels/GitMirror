import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { NgIf, NgFor } from '@angular/common';
import { GenericBanner } from '../../components/generic-banner/generic-banner';
import { RepositoryService } from '../../services/repository/history.service';
import { PaginatedList } from '../../services/pagination/paginated-list.interface';
import { RepositoryResponse } from '../../services/repository/models/repository-response.interface';
import { NgIcon, NgIconComponent } from "@ng-icons/core";
import { CreateRepository } from './create-repository/create-repository';
import { EditRepository } from './edit-repository/edit-repository';
import { DeleteRepository } from './delete-repository/delete-repository';
import { PlatformResponse } from '../../services/platform/models/platform-response.interface';
import { PlatformType } from '../../services/platform/models/platform-type.enum';

@Component({
  selector: 'app-repositories',
  imports: [NgIf, NgFor, NgIcon, NgIconComponent, CreateRepository, EditRepository, DeleteRepository],
  templateUrl: './repositories.html',
  styleUrl: './repositories.css',
})
export class Repositories implements OnInit {
  repositories: PaginatedList<RepositoryResponse> | undefined;
  selectedRepository: RepositoryResponse | undefined;
  routeRepositoryId: string | undefined;

  pageSize = 2147483647;
  page = 0;

  @ViewChild(CreateRepository) addRepositoryDrawer!: CreateRepository;
  @ViewChild(EditRepository) editRepositoryDrawer!: EditRepository;
  @ViewChild(DeleteRepository) deleteRepositoryDrawer!: DeleteRepository;

  constructor(
    private route: ActivatedRoute,
    private repositoryService: RepositoryService
  ) {
    this.routeRepositoryId = this.route.snapshot.paramMap.get('id')!;
  }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.repositories = undefined;
    this.repositoryService.get(this.pageSize, this.page).subscribe({
      next: (x: PaginatedList<RepositoryResponse>) => {
        this.repositories = x

        if (this.routeRepositoryId == undefined) {
          return;
        }

        const selectedRepository = this.repositories.data.find(r => r.id === this.routeRepositoryId);
        if (selectedRepository) {
          this.editRepository(new Event('click'), selectedRepository);
        }
      }
    });
  }

  addRepository(event: Event) {
    event.stopPropagation();
    this.addRepositoryDrawer.drawer.openDrawer(event);
  }

  editRepository(event: Event, repository: RepositoryResponse) {
    event.stopPropagation();
    this.selectedRepository = repository;
    this.editRepositoryDrawer.openDrawer(repository);
    this.editRepositoryDrawer.drawer.openDrawer(event);
  }

  deleteRepository(event: Event, repository: RepositoryResponse) {
    event.stopPropagation();
    this.selectedRepository = repository;
    this.deleteRepositoryDrawer.openDrawer(repository);
    this.deleteRepositoryDrawer.drawer.openDrawer(event);
  }

  previousPage() {
    this.page--;
    this.fetchData();
  }

  nextPage() {
    this.page++;
    this.fetchData();
  }

  getPlatform(platformId: string): RepositoryResponse | undefined {
    return this.repositories?.data.find(p => p.id === platformId);
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
