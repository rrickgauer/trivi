import { CustomEmptyMessage, CustomMessage } from "./custom-events";


export const SuccessfulLoginEvent = new CustomEmptyMessage();
export const SuccessfulSignupEvent = new CustomEmptyMessage();


export const OpenNewCollectionModal = new CustomEmptyMessage();





export type EditCollectionFormSubmittedData = {
    name: string;
}

export const EditCollectionFormSubmittedEvent = new CustomMessage<EditCollectionFormSubmittedData>();
