from base.drivers.base_web_driver import BaseWebDriver
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys

class JsonOperateDriver(BaseWebDriver):
    def __init__(self):
        super().__init__()
        pass

    @classmethod
    def operate_actions(cls, operate_data_list:list = []):
        for operate_data in operate_data_list:
            cls.operate_action(operate_data)
        
    @classmethod
    def operate_action(cls, operate_data:dict = {}):
        action = operate_data['action']
        
        # elementは読込完了を待つ
        if operate_data.get('cssSelector'):
            elm = cls.find_wait_located_element(selector=(By.CSS_SELECTOR, operate_data.get('cssSelector')))
        
        # 各操作を実行する 
        if action == 'jump':
            # 対象のページに移動する
            cls.driver.get(operate_data.get('url'))
        elif action == 'click':
            # クリックする
            elm.click()
        elif action == 'input':
            # 入力種別に応じた入力を行う
            if operate_data.get('elementType') == 'text':
                if operate_data.get("is_add"):
                    cls.input_text_add(elm=elm, value=operate_data.get('text'))
                else:
                    cls.input_text(elm=elm, value=operate_data.get('text'))
            elif operate_data.get('elementType') == 'check':
                cls.input_checkbox(elm=elm, is_check=operate_data.get('isChecked'))
            elif operate_data.get('elementType') == 'radio':
                if 'name' in operate_data:
                    cls.input_radio(elm=elm, name=operate_data.get('name'), value=operate_data.get('text'))
                else: 
                    cls.select_radio(elm=elm)
            elif operate_data.get('elementType') == 'select':
                cls.input_select(elm=elm, value=operate_data.get('text'))
        elif action == 'wait':
            # 指定秒待つ
            cls.wait(wait_seconds=operate_data.get('waitSeconds'))
        elif action == 'switchToWindow':
            # タブを移動する（未指定時、最新タブ）
            index = operate_data.get('index') if 'index' in operate_data else -1
            cls.switch_to_window(index=index)
        