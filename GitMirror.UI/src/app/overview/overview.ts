import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NgIf, NgFor, DatePipe } from '@angular/common';
import { GenericBanner } from '../../components/generic-banner/generic-banner';
import { OverviewService } from '../../services/overview/overview.service';
import { OverviewResponse } from '../../services/overview/models/overview-response.interface';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { NgIcon, NgIconComponent } from "@ng-icons/core";
import { GenericDatagrid, GenericDatagridColumns, GenericDatagridColumn, GenericDatagridRow, GenericDatagridCell } from "../../components/generic-datagrid/generic-datagrid";
import { HistoryState } from '../../services/history/models/history-state.enum';

@Component({
  selector: 'app-overview',
  imports: [RouterLink, GenericBanner, NgIf, NgFor, DatePipe, NgxChartsModule, NgIcon, NgIconComponent, GenericDatagrid, GenericDatagridColumns, GenericDatagridColumn, GenericDatagridRow, GenericDatagridCell],
  templateUrl: './overview.html',
  styleUrl: './overview.css',
})
export class Overview implements OnInit {
  HistoryState = HistoryState;
  overview: OverviewResponse | undefined;

  dailyActivityChartData: any[] = [];
  successRateData: any[] = [];

  constructor(
    private overviewService: OverviewService
  ) { }

  ngOnInit(): void {
    this.overviewService.get().subscribe({
      next: (data) => {
        this.overview = data;
        this.prepareChartData();
      }
    });
  }

  private prepareChartData(): void {
    if (!this.overview) return;

    this.dailyActivityChartData = [
      {
        name: 'Successful',
        series: this.overview.dailyActivity.map(d => ({
          name: new Date(d.date),
          value: d.successful
        }))
      },
      {
        name: 'Failed',
        series: this.overview.dailyActivity.map(d => ({
          name: new Date(d.date),
          value: d.failed
        }))
      }
    ];

    const total = this.overview.successfulCount + this.overview.failedCount;
    const successRate = total > 0 ? Math.round((this.overview.successfulCount / total) * 100) : 0;
    this.successRateData = [
      { name: 'Success Rate', value: successRate }
    ];
  }
}
