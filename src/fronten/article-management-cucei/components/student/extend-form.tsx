"use client"

import { DropDownItem } from "@/types/drop-down-item";
import { RequestStatus } from "@/types/enums/extend-status";
import type { ExtendRequest } from "@/types/extend-request";
import React, { useState } from "react";

type Props = {
    articles: DropDownItem[];
}

export default function ExtendRequestForm({ articles }: Props) {
    const [extendFormData, setExtendFormData] = useState<ExtendRequest>({
        requesterEmail: "",
        studentCode: "",
        article: 0,
        status: RequestStatus.Approved,
        evidenceFileUrl: "",
        justification: ""
    });
    const [selectedFile, setSelectedFile] = useState<File | null>(null);


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

    const handleSubmit = (e: any) => {
        e.preventDefault();
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
                    <label className="block text-sm font-medium text-slate-700" htmlFor="requesterEmail">
                        Requester Email
                        <input
                            id="requesterEmail"
                            name="requesterEmail"
                            type="email"
                            value={extendFormData.requesterEmail}
                            onChange={handleChange}
                            required
                            placeholder="name@university.edu"
                            className="mt-2 w-full rounded-2xl border border-slate-200 bg-white px-4 py-3 text-sm text-slate-900 outline-none transition focus:border-slate-400 focus:ring-2 focus:ring-slate-200"
                        />
                    </label>

                    <label className="block text-sm font-medium text-slate-700" htmlFor="studentCode">
                        Student Code
                        <input
                            id="studentCode"
                            name="studentCode"
                            type="text"
                            value={extendFormData.studentCode}
                            onChange={handleChange}
                            required
                            maxLength={9}
                            placeholder="A01234567"
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
                        className="inline-flex items-center justify-center rounded-full bg-slate-900 px-6 py-3 text-sm font-semibold text-white shadow-sm transition hover:bg-slate-800 focus:outline-none focus:ring-2 focus:ring-slate-300"
                    >
                        Submit Request
                    </button>
                </div>
            </form>
        </section>
    );
}