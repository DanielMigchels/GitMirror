import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NgIf } from '@angular/common';
import { GenericBanner } from '../../components/generic-banner/generic-banner';
import { OverviewService } from '../../services/overview/overview.service';
import { OverviewResponse } from '../../services/overview/models/overview-response.interface';

@Component({
  selector: 'app-overview',
  imports: [RouterLink, GenericBanner, NgIf],
  templateUrl: './overview.html',
  styleUrl: './overview.css',
})
export class Overview implements OnInit {
  overview: OverviewResponse | undefined;

  constructor(
    private overviewService: OverviewService
  ) { }

  ngOnInit(): void {
    this.overviewService.get().subscribe({
      next: (x) => this.overview = x
    });
  }
}
