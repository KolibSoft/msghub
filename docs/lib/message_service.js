import { MessageModel } from "./message_model.js";
import { CatalogueService } from "./modules/catalogue.js";

class MessageService extends CatalogueService {

    /**
     * @param {{(input: RequestInfo | URL, init?: RequestInit | undefined) => Promise<Response>}} fetch 
     * @param {string} uri 
     */
    constructor(fetch, uri) {
        super(json => new MessageModel(json), fetch, uri);
    }

}

export {
    MessageService
}