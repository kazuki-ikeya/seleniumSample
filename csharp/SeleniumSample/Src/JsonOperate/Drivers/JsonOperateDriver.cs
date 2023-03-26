using System.ComponentModel.DataAnnotations;


using System.Text.Json;
using OpenQA.Selenium;
using Common;
using Base.Drivers;
using JsonOperate.Entities;

namespace JsonOperate.Drivers
{
    class JsonOperateDriver : BaseWebDriver
    {
        public JsonOperateDriver()
        {
        }

        public void OperateActions(List<JsonOperateDataEntity> operateDataList)
        {
            foreach (var operateData in operateDataList)
            {
                this.OperateAction(operateData);
            }
        }

        public void OperateAction(JsonOperateDataEntity operateData)
        {

            var action = operateData.Action;

            // 各操作を実行する 
            if (action == "jump")
            {
                // 対象のページに移動する
                this.driver.Url = operateData.Url;
            }
            else if (action == "click")
            {
                // クリックする
                var elm = this.FindWaitLocatedElement(By.CssSelector(operateData.CssSelector));
                elm.Click();
            }
            else if (action == "input")
            {
                // 入力種別に応じた入力を行う
                var elm = this.FindWaitLocatedElement(By.CssSelector(operateData.CssSelector));
                if (operateData.ElementType == "text")
                {
                    if (operateData.IsAdd)
                    {
                        this.InputTextAdd(elm, operateData.Text);
                    }
                    else
                    {
                        this.InputText(elm, operateData.Text);
                    }
                }
                else if (operateData.ElementType == "check")
                {
                    this.InputCheckbox(elm, operateData.IsChecked);
                }
                else if (operateData.ElementType == "radio")
                {
                    if (string.IsNullOrEmpty(operateData.Name))
                    {
                        this.InputRadio(operateData.Name, operateData.Text);
                    }
                    else
                    {
                        this.SelectRadio(elm);
                    }
                }
                else if (operateData.ElementType == "select")
                {
                    this.InputSelect(elm, operateData.Text);
                }
            }
            else if (action == "wait")
            {
                // 指定秒待つ
                this.Wait(operateData.WaitSeconds);
            }
            else if (action == "switchToWindow")
            {
                // タブを移動する（未指定時、最新タブ）
                var index = operateData.Index;
                this.SwitchToWindow(index);
            }
        }


    }
}