import { JsonObject } from "../domain/types/aliases";
import { Nullable } from "./nullable";



export class MappingUtility
{

    public static toFormData(data: object): FormData
    {
        const form = new FormData();

        for (const key in data)
        {
            if (Nullable.hasValue(data[key]))
            {
                form.append(key, data[key]);
            }
            else
            {
                form.append(key, null);
            }
        }

        return form;
    }

    public static toJson(data: object, ignoreEmptyStrings?: boolean): JsonObject
    {
        let newData = ignoreEmptyStrings ? data : this.fixNullValues(data);

        return JSON.stringify(newData);
    }


    private static fixNullValues(data: object): object
    {
        let newData = {};

        for (const key in data)
        {
            let value = data[key];

            if (Nullable.hasValue(value))
            {
                newData[key] = value;
            }
            else
            {
                newData[key] = null;
            }
        }

        return newData;
    }

}