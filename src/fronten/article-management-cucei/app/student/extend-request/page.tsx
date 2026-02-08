import ExtendRequestForm from "@/components/student/extend-form";
import { DropDownItem } from "@/types/drop-down-item";
import { GenericCategoryDto } from "@/types/drop-down-item";

async function getArticles(): Promise<DropDownItem[]> {

    const apiBaseurl = process.env.NEXT_PUBLIC_ARTICLES_API_BASE_URL;

    if (!apiBaseurl) {
        throw new Error("NEXT_PUBLIC_ARTICLES_API_BASE_URL is not defined")
    }

    const result = await fetch(`${apiBaseurl}/dropdown/articles`)
    {
        cache: "force-cache"
    };

    const data = await result.json();

    if (!result.ok) {
        throw new Error("Failed to fetch articles :c");
    }

    return data.map((a: GenericCategoryDto) => ({
        id: a.id,
        label: a.name
    }));
}

export default async function Page() {
    const articles: DropDownItem[] = await getArticles();

    return <ExtendRequestForm articles={articles} />;
}