import { CatalogueFilters } from "../modules/catalogue.js";

class MessageFilters extends CatalogueFilters {

    /** @type {string} */
    state;

    constructor(json) {
        super(json);
        this.state = json?.state ?? null;
    }

}

export {
    MessageFilters
}