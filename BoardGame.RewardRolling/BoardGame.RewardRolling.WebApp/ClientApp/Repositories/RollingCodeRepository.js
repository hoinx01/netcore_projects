import baseRepository from './BaseRepository';
import { jsonUrls } from './UrlConstants'

export default {
    async filter(filterModel) {
        try {
            var filterResult = await baseRepository.get(jsonUrls.rollingCode.filter, { params: filterModel });
            return Promise.resolve(filterResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async upload(file) {
        try {
            let form = new FormData();
            form.append('file', file, file.name);
            var uploadResult = await baseRepository.uploadFile(jsonUrls.rollingCode.uploadExcelFile, form);
            return Promise.resolve(uploadResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    }
}