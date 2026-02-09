import ExtendRequestForm from "@/components/student/extend-form";
import { DropDownItem } from "@/types/drop-down-item";
import { GenericCategoryDto } from "@/types/drop-down-item";

async function getArticles(): Promise<DropDownItem[]> {
    const apiBaseurl = process.env.NEXT_PUBLIC_ARTICLES_API_BASE_URL;

    if (!apiBaseurl) {
        console.error("NEXT_PUBLIC_ARTICLES_API_BASE_URL is not defined");
        throw new Error("NEXT_PUBLIC_ARTICLES_API_BASE_URL is not defined")
    }

    const requestUrl = `${apiBaseurl}/dropdown/articles`;
    console.log(`[getArticles] Fetching from: ${requestUrl}`);
    console.log(`[getArticles] API Base URL: ${apiBaseurl}`);

    try {
        const result = await fetch(requestUrl, {
            cache: "force-cache"
        });

        console.log(`[getArticles] Response status: ${result.status} ${result.statusText}`);
        console.log(`[getArticles] Response headers:`, Object.fromEntries(result.headers.entries()));

        if (!result.ok) {
            const errorBody = await result.text().catch(() => 'Unable to read response body');
            console.error(`[getArticles] Request failed. URL: ${requestUrl}, Status: ${result.status}, Body: ${errorBody}`);
            throw new Error(
                `Failed to fetch articles: ${result.status} ${result.statusText}. ` +
                `Body: ${errorBody}. URL: ${requestUrl}`
            );
        }

        const data = await result.json();
        console.log(`[getArticles] Successfully fetched ${data.length} articles`);

        return data.map((a: GenericCategoryDto) => ({
            id: a.id,
            label: a.name
        }));
    } catch (error) {
        console.error(`[getArticles] Exception occurred:`, error);
        throw error;
    }
}

export default async function Page() {
    const articles: DropDownItem[] = await getArticles();

    return <ExtendRequestForm articles={articles} />;
}