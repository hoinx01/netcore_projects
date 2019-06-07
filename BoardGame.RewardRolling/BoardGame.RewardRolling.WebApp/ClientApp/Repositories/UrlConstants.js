const jsonUrls = {
    upload: "/admin/files",
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
    }
};

export { jsonUrls };