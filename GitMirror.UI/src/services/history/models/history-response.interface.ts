import { HistoryState } from './history-state.enum';

export interface HistoryResponse {
  id: string;
  state: HistoryState;
  createdOnUtc: string;
  mirrorId?: string;
  repositoryId?: string;
  sourceUrl?: string;
  targetUrl?: string;
}
