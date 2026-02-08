"use client"

import { DropDownItem } from "@/types/drop-down-item";
import { RequestStatus } from "@/types/enums/extend-status";
import type { ExtendRequest } from "@/types/extend-request";
import type { StudentDto } from "@/types/student-dto";
import {
    createExtendRequest,
    createOrUpdateStudent,
    uploadEvidence,
} from "@/services/article-management-client";
import React, { useState } from "react";

type Props = {
    articles: DropDownItem[];
}

type StudentFormData = {
    studentCode: string;
    firstName: string;
    secondName: string;
    lastname: string;
    secondLastName: string;
    email: string;
    careerStart: string;
}

export default function ExtendRequestForm({ articles }: Props) {
    const [studentFormData, setStudentFormData] = useState<StudentFormData>({
        studentCode: "",
        firstName: "",
        secondName: "",
        lastname: "",
        secondLastName: "",
        email: "",
        careerStart: "",
    });
    const [extendFormData, setExtendFormData] = useState<ExtendRequest>({
        studentCode: "",
        article: 0,
        status: RequestStatus.Approved,
        evidenceFileUrl: "",
        justification: ""
    });
    const [selectedFile, setSelectedFile] = useState<File | null>(null);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [submitError, setSubmitError] = useState<string | null>(null);
    const [submitSuccess, setSubmitSuccess] = useState<string | null>(null);


    const handleStudentChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;

        setStudentFormData(prev => ({
            ...prev,
            [name]: value,
        }));

        if (name === "studentCode") {
            setExtendFormData(prev => ({
                ...prev,
                studentCode: value,
            }));
        }
    };


    const handleChange = (
        e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        const { name, value } = e.target;

        setExtendFormData(prev => ({
            ...prev,
            [name]:
                name === "article" || name === "status"
                    ? Number(value)
                    : value
        }))

    };

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0] ?? null;
        setSelectedFile(file);
    };

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        setSubmitError(null);
        setSubmitSuccess(null);

        if (!selectedFile) {
            setSubmitError("Please attach an evidence file before submitting.");
            return;
        }

        if (extendFormData.article === 0) {
            setSubmitError("Please select an article before submitting.");
            return;
        }

        setIsSubmitting(true);

        try {
            const studentPayload: StudentDto = {
                StudentCode: studentFormData.studentCode,
                FirstName: studentFormData.firstName,
                SecondName: studentFormData.secondName,
                Lastname: studentFormData.lastname,
                SecondLastName: studentFormData.secondLastName,
                Email: studentFormData.email,
                CareerStart: studentFormData.careerStart,
            };

            const savedStudent = await createOrUpdateStudent(studentPayload);

            const extendId = await createExtendRequest({
                StudentCode: savedStudent.StudentCode ?? studentPayload.StudentCode,
                Article: extendFormData.article,
                EvidenceFileUrl: "",
                Reason: extendFormData.justification,
            });

            const uploadResult = await uploadEvidence(extendId, selectedFile);

            setExtendFormData(prev => ({
                ...prev,
                evidenceFileUrl: uploadResult.FileUrl,
            }));
            setSubmitSuccess("Request submitted successfully.");
        } catch (error) {
            const message =
                error instanceof Error
                    ? error.message
                    : "Failed to submit the request.";
            setSubmitError(message);
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <section className="min-h-screen bg-[radial-gradient(circle_at_top,rgba(16,24,40,0.06),transparent_55%),linear-gradient(180deg,#f8fafc,#ffffff)] px-4 py-10 sm:px-8">
            <form
                onSubmit={handleSubmit}
                className="mx-auto w-full max-w-xl rounded-3xl border border-slate-200/70 bg-white/80 p-6 shadow-[0_20px_70px_-50px_rgba(15,23,42,0.55)] backdrop-blur sm:p-10"
            >
                <header className="mb-8 space-y-2 text-center">
                    <p className="text-xs font-semibold uppercase tracking-[0.2em] text-slate-500">
                        Student Portal
                    </p>
                    <h1 className="text-2xl font-semibold text-slate-900 sm:text-3xl">
                        Extend Request
                    </h1>
                    <p className="text-sm text-slate-500">
                        Submit a short request and we will review it quickly.
                    </p>
                </header>

                <fieldset className="space-y-5">
                    <label className="block text-sm font-medium text-slate-700" htmlFor="studentCode">
                        Student Code
                        <input
                            id="studentCode"
                            name="studentCode"
                            type="text"
                            value={studentFormData.studentCode}
                            onChange={handleStudentChange}
                            required
                            maxLength={9}
                            placeholder="A01234567"
                            className="mt-2 w-full rounded-2xl border border-slate-200 bg-white px-4 py-3 text-sm text-slate-900 outline-none transition focus:border-slate-400 focus:ring-2 focus:ring-slate-200"
                        />
                    </label>

                    <label className="block text-sm font-medium text-slate-700" htmlFor="firstName">
                        First Name
                        <input
                            id="firstName"
                            name="firstName"
                            type="text"
                            value={studentFormData.firstName}
                            onChange={handleStudentChange}
                            required
                            placeholder="Maria"
                            className="mt-2 w-full rounded-2xl border border-slate-200 bg-white px-4 py-3 text-sm text-slate-900 outline-none transition focus:border-slate-400 focus:ring-2 focus:ring-slate-200"
                        />
                    </label>

                    <label className="block text-sm font-medium text-slate-700" htmlFor="secondName">
                        Second Name
                        <input
                            id="secondName"
                            name="secondName"
                            type="text"
                            value={studentFormData.secondName}
                            onChange={handleStudentChange}
                            required
                            placeholder="Guadalupe"
                            className="mt-2 w-full rounded-2xl border border-slate-200 bg-white px-4 py-3 text-sm text-slate-900 outline-none transition focus:border-slate-400 focus:ring-2 focus:ring-slate-200"
                        />
                    </label>

                    <label className="block text-sm font-medium text-slate-700" htmlFor="lastname">
                        Last Name
                        <input
                            id="lastname"
                            name="lastname"
                            type="text"
                            value={studentFormData.lastname}
                            onChange={handleStudentChange}
                            required
                            placeholder="Hernandez"
                            className="mt-2 w-full rounded-2xl border border-slate-200 bg-white px-4 py-3 text-sm text-slate-900 outline-none transition focus:border-slate-400 focus:ring-2 focus:ring-slate-200"
                        />
                    </label>

                    <label className="block text-sm font-medium text-slate-700" htmlFor="secondLastName">
                        Second Last Name
                        <input
                            id="secondLastName"
                            name="secondLastName"
                            type="text"
                            value={studentFormData.secondLastName}
                            onChange={handleStudentChange}
                            required
                            placeholder="Lopez"
                            className="mt-2 w-full rounded-2xl border border-slate-200 bg-white px-4 py-3 text-sm text-slate-900 outline-none transition focus:border-slate-400 focus:ring-2 focus:ring-slate-200"
                        />
                    </label>

                    <label className="block text-sm font-medium text-slate-700" htmlFor="email">
                        Student Email
                        <input
                            id="email"
                            name="email"
                            type="email"
                            value={studentFormData.email}
                            onChange={handleStudentChange}
                            required
                            placeholder="name@university.edu"
                            className="mt-2 w-full rounded-2xl border border-slate-200 bg-white px-4 py-3 text-sm text-slate-900 outline-none transition focus:border-slate-400 focus:ring-2 focus:ring-slate-200"
                        />
                    </label>

                    <label className="block text-sm font-medium text-slate-700" htmlFor="careerStart">
                        Career Start
                        <input
                            id="careerStart"
                            name="careerStart"
                            type="date"
                            value={studentFormData.careerStart}
                            onChange={handleStudentChange}
                            required
                            className="mt-2 w-full rounded-2xl border border-slate-200 bg-white px-4 py-3 text-sm text-slate-900 outline-none transition focus:border-slate-400 focus:ring-2 focus:ring-slate-200"
                        />
                    </label>

                    <label className="block text-sm font-medium text-slate-700" htmlFor="article">
                        Article
                        <select
                            id="article"
                            name="article"
                            value={extendFormData.article}
                            onChange={(e) =>
                                setExtendFormData((prev) => ({
                                    ...prev,
                                    article: Number(e.target.value),
                                }))
                            }
                            className="mt-2 w-full rounded-2xl border border-slate-200 bg-white px-4 py-3 text-sm text-slate-900 outline-none transition focus:border-slate-400 focus:ring-2 focus:ring-slate-200"
                        >
                            <option value={0}>Select an article</option>
                            {articles.map((a: DropDownItem) => (
                                <option key={a.id} value={a.id}>
                                    {a.label}
                                </option>
                            ))}
                        </select>
                    </label>

                    <label className="block text-sm font-medium text-slate-700" htmlFor="evidenceFile">
                        Evidence File
                        <input
                            id="evidenceFile"
                            name="evidenceFile"
                            type="file"
                            onChange={handleFileChange}
                            className="mt-2 w-full rounded-2xl border border-dashed border-slate-200 bg-white px-4 py-3 text-sm text-slate-600 file:mr-4 file:rounded-full file:border-0 file:bg-slate-900 file:px-4 file:py-2 file:text-xs file:font-semibold file:text-white hover:file:bg-slate-800"
                        />
                        <span className="mt-1 block text-xs text-slate-400">
                            Attach any supporting document.
                        </span>
                    </label>

                    <label className="block text-sm font-medium text-slate-700" htmlFor="justification">
                        Justification
                        <textarea
                            id="justification"
                            name="justification"
                            value={extendFormData.justification}
                            onChange={handleChange}
                            maxLength={420}
                            rows={4}
                            placeholder="Keep it short and clear."
                            className="mt-2 w-full resize-none rounded-2xl border border-slate-200 bg-white px-4 py-3 text-sm text-slate-900 outline-none transition focus:border-slate-400 focus:ring-2 focus:ring-slate-200"
                        />
                        <span className="mt-1 block text-xs text-slate-400">
                            420 characters max.
                        </span>
                    </label>
                </fieldset>

                <div className="mt-8 flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
                    <p className="text-xs text-slate-400">
                        We will email you once it is reviewed.
                    </p>
                    <button
                        type="submit"
                        disabled={isSubmitting}
                        className="inline-flex items-center justify-center rounded-full bg-slate-900 px-6 py-3 text-sm font-semibold text-white shadow-sm transition hover:bg-slate-800 focus:outline-none focus:ring-2 focus:ring-slate-300"
                    >
                        {isSubmitting ? "Submitting..." : "Submit Request"}
                    </button>
                </div>
                {(submitError || submitSuccess) && (
                    <p
                        className={`mt-4 text-sm ${
                            submitError ? "text-rose-600" : "text-emerald-600"
                        }`}
                    >
                        {submitError ?? submitSuccess}
                    </p>
                )}
            </form>
        </section>
    );
}