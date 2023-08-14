import { MessageModel } from "./message_model.js";
import { DatabaseCatalogue, DbContext } from "./modules/catalogue.js";

class MessageDatabaseCatalogue extends DatabaseCatalogue {

    /**
     * @param {DbContext} dbContext 
     */
    constructor(dbContext) {
        super(json => new MessageModel(json), dbContext, "message");
    }

}

export {
    MessageDatabaseCatalogue
}