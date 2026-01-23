import { HistoryState } from './history-state.enum';

export interface HistoryResponse {
  id: string;
  state: HistoryState;
  createdOnUtc: string;
  mirrorId?: string;
  repositoryId?: string;
  sourceType?: number;
  sourceBaseUrl?: string;
  targetType?: number;
  targetBaseUrl?: string;
}
