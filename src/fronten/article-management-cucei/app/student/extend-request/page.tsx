import ExtendRequestForm from "@/components/student/extend-form";
import { DropDownItem } from "@/types/drop-down-item";
import { GenericCategoryDto } from "@/types/drop-down-item";

// Force dynamic rendering - don't prerender at build time
export const dynamic = 'force-dynamic';

async function getArticles(): Promise<DropDownItem[]> {
    const apiBaseurl = process.env.NEXT_PUBLIC_ARTICLES_API_BASE_URL;

    if (!apiBaseurl) {
        console.error("NEXT_PUBLIC_ARTICLES_API_BASE_URL is not defined");
        return []; // Return empty array instead of throwing
    }

    const requestUrl = `${apiBaseurl}/dropdown/articles`;
    console.log(`[getArticles] Fetching from: ${requestUrl}`);

    try {
        const result = await fetch(requestUrl, {
            cache: "no-store" // Don't cache, fetch fresh data
        });

        if (!result.ok) {
            console.error(`[getArticles] Request failed. Status: ${result.status}`);
            return []; // Return empty array on error
        }

        const data = await result.json();
        return data.map((a: GenericCategoryDto) => ({
            id: a.id,
            label: a.name
        }));
    } catch (error) {
        console.error(`[getArticles] Exception occurred:`, error);
        return []; // Return empty array on exception
    }
}

export default async function Page() {
    const articles: DropDownItem[] = await getArticles();
    return <ExtendRequestForm articles={articles} />;
}