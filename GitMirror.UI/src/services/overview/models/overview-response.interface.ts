import { DailyActivity } from './daily-activity.interface';
import { RecentHistory } from './recent-history.interface';

export interface OverviewResponse {
  repositoryCount: number;
  mirrorCount: number;
  platformCount: number;
  historyCount: number;
  queuedCount: number;
  inProgressCount: number;
  successfulCount: number;
  failedCount: number;
  dailyActivity: DailyActivity[];
  recentHistory: RecentHistory[];
  isDemoMode: boolean;
}
