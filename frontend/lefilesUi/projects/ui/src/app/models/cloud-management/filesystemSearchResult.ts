import { FileAndFoldersResponse, FileSystemEntryItemResponse } from "../file-management/fileSystemEntryItemResponse";

export interface FilesystemSearchResult {
    searchText:string,
    result:FileSystemEntryItemResponse[]
}