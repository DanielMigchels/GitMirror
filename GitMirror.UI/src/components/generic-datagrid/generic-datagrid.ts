import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PaginatedList } from '../../services/pagination/paginated-list.interface';
import { NgFor, NgIf, DatePipe, KeyValuePipe, NgClass } from '@angular/common';

@Component({
  selector: 'app-generic-datagrid',
  imports: [NgFor, NgIf, NgClass],
  templateUrl: './generic-datagrid.html',
  styleUrl: './generic-datagrid.css',
})
export class GenericDatagrid {
  @Input() data?: PaginatedList<any>;
  @Output() next = new EventEmitter<void>();
  @Output() previous = new EventEmitter<void>();

  getKeys(item: any): string[] {
    return Object.keys(item);
  }

  onNext(): void {
    this.next.emit();
  }

  onPrevious(): void {
    this.previous.emit();
  }

  formatValue(value: any): string {
    if (value === null || value === undefined) {
      return '-';
    }
    if (typeof value === 'boolean') {
      return value ? 'Yes' : 'No';
    }
    if (typeof value === 'object' && value instanceof Date) {
      return value.toLocaleString();
    }
    if (typeof value === 'string' && this.isISODate(value)) {
      return new Date(value).toLocaleString();
    }
    return String(value);
  }

  private isISODate(value: string): boolean {
    const isoDatePattern = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}/;
    return isoDatePattern.test(value);
  }

  formatKey(key: string): string {
    return key
      .replace(/([A-Z])/g, ' $1')
      .replace(/^./, (str) => str.toUpperCase())
      .trim();
  }
}
