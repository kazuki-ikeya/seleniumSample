import datetime
import json
from common.file import CommonFile

class CommonCalendar():
    def __init__(self):
        # 祝日情報を読み込み
        self.load_holidays()
        pass
    
    holidays :dict = {}
    
    @classmethod
    def load_holidays(cls):
        if cls.holidays:
            return cls.holidays
        target_file_path = 'src/common/jsons/holidays.json'
        file_path = CommonFile.create_abs_path(target_file_path)
        
        file_data = CommonFile.read(file_path)
        json_data = json.loads(file_data)
        cls.holidays = json_data
        return cls.holidays
    
    @classmethod
    def is_holiday(cls, date=None):
        cls.load_holidays()
        
        # 未指定時は今日で判定
        if not date:
            date = datetime.date.today()

        # 土日判定
        if date.weekday() in [5, 6]:
            return True

        # 祝日判定
        str_date = date.strftime('%Y-%m-%d')
        if str_date in cls.holidays:
            return True
        
        return False
        
        