﻿const jsonUrls = {
    upload: "/admin/api/files",
    rollingCode: {
        filter: "/admin/api/rolling_codes.json",
        uploadExcelFile: "/admin/api/rolling_codes/excel_files.json"
    },
    reward: {
        filter: "/admin/api/rewards.json",
        getById: "/admin/api/rewards/{id}.json",
        update: "/admin/api/rewards/{id}.json",
        delete: "/admin/api/rewards/{id}.json",
        add: "/admin/api/rewards.json"
    },
    campaign: {
        filter: "/admin/api/campaigns.json",
        getById: "/admin/api/campaigns/{id}.json",
        update: "/admin/api/campaigns/{id}.json",
        delete: "/admin/api/campaigns/{id}.json",
        add: "/admin/api/campaigns.json"
    },
    user: {
        login: "/admin/api/users/login.json",
        getCurrentUser: "/admin/api/users/me.json",
        createUser: "/admin/api/users.json"
    },
    customer: {
        filter: "/admin/api/customers.json",
        getById: "/admin/api/customers/{id}.json",
        exportExcel: "/admin/api/customers/export_excel.json"
    },
    administrativeUnit: {
        uploadStandardFile: "/admin/api/administrative_units/excel_files"
    },
    city: {
        getAll: "/admin/api/cities.json"
    },
    district: {
        getDistrictByCityId: "/admin/api/districts.json"
    },
    commune: {
        getCommuneByDistrictId: "/admin/api/communes.json"
    }
};

export { jsonUrls };