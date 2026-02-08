import { RequestStatus } from "./enums/extend-status";

export type ExtendRequest = {
  studentCode: string;
  article: number;
  status: RequestStatus;
  evidenceFileUrl: string;
  justification: string;
};
