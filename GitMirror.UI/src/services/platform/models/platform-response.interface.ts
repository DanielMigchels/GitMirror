import { PlatformType } from './platform-type.enum';

export interface PlatformResponse {
  id: string;
  type: PlatformType;
  username: string;
  baseUrl: string;
}
