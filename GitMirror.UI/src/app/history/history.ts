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
import { NgFor, NgIf, DatePipe } from '@angular/common';
import { GenericDatagrid, GenericDatagridColumns, GenericDatagridColumn, GenericDatagridRow, GenericDatagridCell } from "../../components/generic-datagrid/generic-datagrid";

@Component({
  selector: 'app-history',
  imports: [RouterLink, GenericBanner, NgIf, NgFor, DatePipe, GenericDatagrid, GenericDatagridColumns, GenericDatagridColumn, GenericDatagridRow, GenericDatagridCell],
  templateUrl: './history.html',
  styleUrl: './history.css',
})
export class History {
  mirrors: PaginatedList<MirrorResponse> | undefined;
  repositories: PaginatedList<RepositoryResponse> | undefined;
  history: PaginatedList<HistoryResponse> | undefined;
  currentPage = 0;
  pageSize = 12;

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

    this.loadHistory();
  }

  loadHistory(): void {
    this.historyService.get(this.pageSize, this.currentPage).subscribe({
      next: (x) => this.history = x
    });
  }

  onNext(): void {
    if (this.history?.hasNext) {
      this.currentPage++;
      this.loadHistory();
    }
  }

  onPrevious(): void {
    if (this.history?.hasPrevious) {
      this.currentPage--;
      this.loadHistory();
    }
  }
}
