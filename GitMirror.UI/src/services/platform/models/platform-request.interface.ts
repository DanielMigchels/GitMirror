import { PlatformType } from './platform-type.enum';

export interface PlatformRequest {
  type: PlatformType;
  username: string;
  password: string;
  baseUrl: string;
}
