
using System.Text.Json;

using OpenQA.Selenium;
using Common;
using JsonOperate.Drivers;
using JsonOperate.Entities;

namespace JsonOperate.Actions
{
    class JsonOperateMain : JsonOperateDriver
    {

        public JsonOperateMain()
        {
        }

        public void Main(string path)
        {
            // 操作内容を決定する
            var operateDataList = this.LoadOperateDataList(path);

            // jsonアクション実行
            this.OperateActions(operateDataList);

        }

        // 入力内容を決定する

        public List<JsonOperateDataEntity> LoadOperateDataList(string targetFilePath) {
            var filePath = CommonFile.CreateAbsPath(targetFilePath);
            var fileData = CommonFile.Read(filePath);
            var tempJsonData = JsonSerializer.Deserialize(fileData, typeof(JsonOperateEntity));
        
            if (tempJsonData is null){
                throw new Exception("JSONデータの取込に失敗しました。");
            }
            var jsonDataList = ((JsonOperateEntity)tempJsonData).DataList;
            return jsonDataList;
        }


    }
}