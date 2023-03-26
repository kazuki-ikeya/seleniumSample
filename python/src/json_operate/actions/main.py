from json_operate.drivers.json_operate_driver import JsonOperateDriver
import json
from common.file import CommonFile
from common.util import CommonUtil
from selenium.webdriver.remote.webelement import WebElement
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.common.by import By

class Main(JsonOperateDriver):
    def __init__(self):
        super().__init__()
        pass
    
    @classmethod
    def main(cls, file_path:str):
        
        # 操作内容を決定する
        operate_data_list = cls.load_operate_data_list(file_path)

        # jsonアクション実行
        cls.operate_actions(operate_data_list)
        
        
    # 入力内容を決定する
    @classmethod
    def load_operate_data_list(cls, target_file_path:str):
        file_path = CommonFile.create_abs_path(target_file_path)
        file_data = CommonFile.read(file_path)
        temp_json_data = json.loads(file_data)
        json_data_list = temp_json_data.get("dataList")
        
        return json_data_list
