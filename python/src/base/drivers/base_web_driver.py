import os

from datetime import datetime
from selenium import webdriver
from selenium.webdriver.chrome.webdriver import WebDriver
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.remote.webelement import WebElement
from selenium.webdriver.support.select import Select
import time
from common.file import CommonFile
from webdriver_manager.chrome import ChromeDriverManager

class BaseWebDriver:
    def __init__(cls):
        pass
    
    driver: WebDriver = None
    
    @classmethod
    def start_driver(cls):
        options = webdriver.ChromeOptions()
        options.add_argument('--lang=ja-JP')
        options.add_experimental_option('detach', True)
        cls.driver = webdriver.Chrome(ChromeDriverManager().install(),options=options)
        cls.driver.maximize_window()

    @classmethod
    def stop_driver(cls):
        cls.driver.quit()
    
    @classmethod
    def log_current_url(cls):
        print('current URL: ', cls.driver.current_url)

    @classmethod
    def get_screenshot(cls, id: str = "temp", 
                       prefix: str = "", suffix: str = ""):
        now = datetime.now()
        prefix = f"{prefix}_" if prefix else ""
        suffix = f"_{suffix}" if suffix else ""
        file_name = f"{id}_{prefix}{now.strftime('%Y%m%d%H%M%S%f')}{suffix}.png"
        dir_path = f"{os.getcwd()}/screenshots/"
        file_path = f"{dir_path}{file_name}"
        
        if not CommonFile.exists_path(dir_path):
            CommonFile.make_directory(dir_path)

        print(f'screenshot:{file_path}')
        screenshot = cls.driver.get_screenshot_as_png()
    
        CommonFile.make(file_path, screenshot, mode='wb+')

    @classmethod
    def switch_to_window(cls, index=-1):
        # seleniumが速すぎるため少し待つ
        cls.wait(0.5)
        
        windows = cls.driver.window_handles
        if index == -1:
            index = len(windows) - 1
        cls.driver.switch_to.window(windows[index])

    @classmethod
    def find_wait_clickable_element(cls, selector, wait_seconds=5):
        wait = WebDriverWait(cls.driver, wait_seconds)
        elm = wait.until(EC.element_to_be_clickable(selector))
        return elm
    
    @classmethod
    def find_wait_located_element(cls, selector, wait_seconds=10):
        wait = WebDriverWait(cls.driver, wait_seconds)
        elm = wait.until(EC.visibility_of_element_located(selector))
        return elm

    @classmethod
    def wait_loading(cls, selector, wait_seconds=10):
        # 判定用のエレメントが読込されるまで待機する
        wait = WebDriverWait(cls.driver, wait_seconds)
        elm = wait.until(EC.visibility_of_element_located(selector))
    
    @classmethod
    def wait(cls, wait_seconds=1):
        time.sleep(wait_seconds)
    
    @classmethod
    def clear_text(cls, elm: WebElement):
        elm.clear()
        
    @classmethod
    def input_text(cls, elm: WebElement, value):
        cls.clear_text(elm)
        elm.send_keys(value)
    
    @classmethod
    def input_text_add(cls, elm: WebElement, value):
        elm.send_keys(value)
        
    @classmethod
    def input_checkbox(cls, elm: WebElement, is_check: bool):
        is_checked = elm.is_selected()
        if is_checked == is_check:
            return
        elm.click()

    @classmethod
    def input_select(cls, elm: WebElement, value):
        Select(elm).select_by_value(value)
    
    @classmethod
    def input_radio(cls, name: str , value):
        elm_list = cls.driver.find_elements(by=By.NAME, value=name)
        elm = next(filter(lambda x:x.get_attribute('value') == value, elm_list))
        elm.click()
        
    @classmethod
    def select_radio(cls, elm: WebElement):
        elm.click()
    