import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { GenericBanner } from '../../components/generic-banner/generic-banner';
import { HistoryService } from '../../services/history/history.service';
import { HistoryResponse } from '../../services/history/models/history-response.interface';
import { MirrorService } from '../../services/mirror/mirror.service';
import { MirrorResponse } from '../../services/mirror/models/mirror-response.interface';
import { PaginatedList } from '../../services/pagination/paginated-list.interface';
import { RepositoryService } from '../../services/repository/history.service';
import { RepositoryResponse } from '../../services/repository/models/repository-response.interface';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-history',
  imports: [RouterLink, GenericBanner, NgIf],
  templateUrl: './history.html',
  styleUrl: './history.css',
})
export class History {
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
