import { IController } from "../../domain/contracts/icontroller";
import { MessageBoxUtility } from "../../utility/message-box-utility";



export class HomePageController implements IController
{
    public control()
    {
        MessageBoxUtility.showStandard({
            message: 'Message',
        });
    }
}