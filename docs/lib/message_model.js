import { Item } from "./modules/catalogue.js";

class MessageModel extends Item {

    /** @type {string} */
    sender;

    /** @type {string} */
    receiver;

    /** @type {string} */
    content;

    /** @type {string} */
    state;

    constructor(json) {
        super(json);
        this.sender = json?.sender ?? "";
        this.receiver = json?.receiver ?? "";
        this.content = json?.content ?? "";
        this.state = json?.state ?? "";
    }

}

export {
    MessageModel
}