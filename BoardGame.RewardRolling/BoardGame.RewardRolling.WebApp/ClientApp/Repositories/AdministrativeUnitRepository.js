import baseRepository from './BaseRepository';
import { jsonUrls } from './UrlConstants'
import string from '../Utils/StringExtensions';

export default {
    async uploadStandardFile(file) {
        try {
            let form = new FormData();
            form.append('file', file, file.name);
            var uploadResult = await baseRepository.uploadFile(jsonUrls.administrativeUnit.uploadStandardFile, form);
            return Promise.resolve(uploadResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    }
}