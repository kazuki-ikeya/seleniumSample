

class CommonUtil():
    @staticmethod
    def get_class_properties(target_class):
        result = {}
        properties = vars(target_class)
        for key, value in properties.items():
            # プライベート項目は除外
            if key.startswith('_'):
                continue
            result[key] = value
        return result