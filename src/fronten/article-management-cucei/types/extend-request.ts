import { RequestStatus } from "./enums/extend-status";

export type ExtendRequest = {
  requesterEmail: string;
  studentCode: string;
  article: number;
  status: RequestStatus;
  evidenceFileUrl: string;
  justification: string;
};
