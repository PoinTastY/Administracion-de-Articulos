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

    const handleSubmit = (e: any) => {
        console.log("Submitting: " + extendFormData)
    };

    return (
        <form onSubmit={handleSubmit}>
            <fieldset>
                <legend>Extend Request</legend>
                <br />

                <div>
                    <label htmlFor="requesterEmail">Requester Email</label>
                    <br />
                    <input
                        id="requesterEmail"
                        name="requesterEmail"
                        type="email"
                        value={extendFormData.requesterEmail}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div>
                    <label htmlFor="studentCode">Student Code</label>
                    <br />
                    <input
                        id="studentCode"
                        name="studentCode"
                        type="text"
                        value={extendFormData.studentCode}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div>
                    <label>
                        Article
                        <select
                            name="article"
                            value={extendFormData.article}
                            onChange={(e: React.ChangeEvent<HTMLSelectElement, HTMLSelectElement>) =>
                                setExtendFormData((prev) => ({
                                    ...prev,
                                    article: Number(e.target.value),
                                }))
                            }
                        >
                            <option value={0}>Select an article</option>
                            {articles.map((a: DropDownItem) => (
                                <option key={a.id} value={a.id}>
                                    {a.label}
                                </option>
                            ))}
                        </select>

                    </label>
                </div>

                <div>
                    <label htmlFor="justification">Justification</label>
                    <br />
                    <textarea
                        id="justification"
                        name="justification"
                        value={extendFormData.justification}
                        onChange={handleChange}
                        maxLength={420}
                        rows={4}
                    />
                </div>

                <div>
                    <button type="submit">Submit Request</button>
                </div>
            </fieldset>
        </form>
    );
}