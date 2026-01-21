import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NgIf } from '@angular/common';
import { GenericBanner } from '../../components/generic-banner/generic-banner';
import { PlatformService } from '../../services/platform/platform.service';
import { MirrorService } from '../../services/mirror/mirror.service';
import { HistoryService } from '../../services/history/history.service';
import { PaginatedList } from '../../services/pagination/paginated-list.interface';
import { PlatformResponse } from '../../services/platform/models/platform-response.interface';
import { MirrorResponse } from '../../services/mirror/models/mirror-response.interface';
import { HistoryResponse } from '../../services/history/models/history-response.interface';
import { RepositoryResponse } from '../../services/repository/models/repository-response.interface';
import { RepositoryService } from '../../services/repository/history.service';

@Component({
  selector: 'app-overview',
  imports: [RouterLink, GenericBanner, NgIf],
  templateUrl: './overview.html',
  styleUrl: './overview.css',
})
export class Overview implements OnInit {
  mirrors: PaginatedList<MirrorResponse> | undefined;
  repositories: PaginatedList<RepositoryResponse> | undefined;
  history: PaginatedList<HistoryResponse> | undefined;

  constructor(
    private mirrorService: MirrorService,
    private repositoryService: RepositoryService,
    private historyService: HistoryService
  ) { }

  ngOnInit(): void {
    this.mirrorService.get(1, 0).subscribe({
      next: (x) => this.mirrors = x
    });

    this.repositoryService.get(1, 0).subscribe({
      next: (x) => this.repositories = x
    });

    this.historyService.get(1, 0).subscribe({
      next: (x) => this.history = x
    });
  }
}
