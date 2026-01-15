import { HistoryState } from './history-state.enum';

export interface HistoryRequest {
  state: HistoryState;
  mirrorId?: string;
  repositoryId?: string;
}
