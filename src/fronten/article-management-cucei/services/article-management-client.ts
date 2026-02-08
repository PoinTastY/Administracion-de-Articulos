import type { StudentDto } from "@/types/student-dto";

type ExtendRequestPayload = {
  StudentCode: string;
  Article: number;
  EvidenceFileUrl: string;
  Reason: string;
  Status?: string;
};

type UploadEvidenceResponse = {
  FileUrl: string;
};

type ExtendRequestResponse = {
  id?: number;
  Id?: number;
  extendId?: number;
  extendRequestId?: number;
};

type StudentResponse = StudentDto | { student: StudentDto; message: string };

const getApiBaseUrl = () => {
  const apiBaseUrl = process.env.NEXT_PUBLIC_ARTICLES_API_BASE_URL;

  if (!apiBaseUrl) {
    throw new Error("NEXT_PUBLIC_ARTICLES_API_BASE_URL is not defined");
  }

  return apiBaseUrl;
};

const parseStudentResponse = (payload: StudentResponse): StudentDto => {
  if ("student" in payload) {
    return payload.student;
  }

  return payload;
};

const parseExtendIdFromLocation = (location: string): number | null => {
  const parts = location.split("/").filter(Boolean);
  const candidate = Number(parts[parts.length - 1]);

  if (!Number.isNaN(candidate) && Number.isFinite(candidate)) {
    return candidate;
  }

  return null;
};

export const createOrUpdateStudent = async (
  student: StudentDto,
): Promise<StudentDto> => {
  const apiBaseUrl = getApiBaseUrl();
  const response = await fetch(`${apiBaseUrl}/student`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(student),
  });

  const payload = (await response.json()) as StudentResponse;

  if (!response.ok) {
    throw new Error("Failed to create or update student");
  }

  return parseStudentResponse(payload);
};

export const createExtendRequest = async (
  payload: ExtendRequestPayload,
): Promise<number> => {
  const apiBaseUrl = getApiBaseUrl();
  const response = await fetch(`${apiBaseUrl}/extension`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });

  if (!response.ok) {
    throw new Error("Failed to create extend request");
  }

  const location = response.headers.get("location");

  if (location) {
    const extendId = parseExtendIdFromLocation(location);
    if (extendId !== null) {
      return extendId;
    }
  }

  const responseText = await response.text();
  if (responseText.trim()) {
    try {
      const parsed = JSON.parse(responseText) as ExtendRequestResponse;
      const extendId =
        parsed.id ?? parsed.Id ?? parsed.extendId ?? parsed.extendRequestId;
      if (typeof extendId === "number") {
        return extendId;
      }
    } catch {
      // Ignore JSON parsing errors and fall through to generic error.
    }
  }

  throw new Error("Extend request created but no id was returned");
};

export const uploadEvidence = async (
  extendId: number,
  file: File,
): Promise<UploadEvidenceResponse> => {
  const apiBaseUrl = getApiBaseUrl();
  const formData = new FormData();
  formData.append("file", file);

  const response = await fetch(
    `${apiBaseUrl}/extension/${extendId}/upload-evidence`,
    {
      method: "POST",
      body: formData,
    },
  );

  if (!response.ok) {
    throw new Error("Failed to upload evidence file");
  }

  return (await response.json()) as UploadEvidenceResponse;
};
