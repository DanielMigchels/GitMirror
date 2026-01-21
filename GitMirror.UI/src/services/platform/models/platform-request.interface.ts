import { PlatformType } from './platform-type.enum';

export interface PlatformRequest {
  type: number;
  username: string;
  password: string;
  baseUrl: string;
}
