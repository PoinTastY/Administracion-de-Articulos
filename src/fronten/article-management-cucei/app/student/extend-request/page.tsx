import ExtendRequestForm from "@/components/student/extend-form";
import { DropDownItem } from "@/types/drop-down-item";
import { GenericCategoryDto } from "@/types/drop-down-item";

export const dynamic = "force-dynamic";

async function getArticles(): Promise<DropDownItem[]> {
    const apiBaseurl = process.env.NEXT_PUBLIC_ARTICLES_API_BASE_URL;

    if (!apiBaseurl) {
        throw new Error("NEXT_PUBLIC_ARTICLES_API_BASE_URL is not defined");
    }

    const result = await fetch(`${apiBaseurl}/dropdown/articles`, {
        cache: "no-store",
        headers: {
            Accept: "application/json",
        },
    });

    if (!result.ok) {
        const errorBody = await result.text();
        throw new Error(
            `Failed to fetch articles (${result.status}): ${errorBody.slice(0, 200)}`
        );
    }

    const contentType = result.headers.get("content-type") ?? "";
    if (!contentType.includes("application/json")) {
        const errorBody = await result.text();
        throw new Error(
            `Expected JSON but got ${contentType || "unknown"}: ${errorBody.slice(0, 200)}`
        );
    }

    const data = await result.json();

    return data.map((a: GenericCategoryDto) => ({
        id: a.id,
        label: a.name
    }));
}

export default async function Page() {
    let articles: DropDownItem[] = [];
    let articlesError: string | null = null;

    try {
        articles = await getArticles();
    } catch (error) {
        articlesError =
            error instanceof Error
                ? error.message
                : "Failed to load articles.";
    }

    return <ExtendRequestForm articles={articles} articlesError={articlesError} />;
}