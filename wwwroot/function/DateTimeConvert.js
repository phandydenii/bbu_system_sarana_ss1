const formatDateTime = (date, format = 'YYYY-MM-DD HH:mm:ss') => {
    return moment(date).format(format);
};

const parseDateTime = (dateString) => {
    return moment(dateString).toDate();
};

const getRelativeTime = (date) => {
    return moment(date).fromNow();
};

const formatDateOnly = (date) => {
    return moment(date).format('YYYY-MM-DD');
};

const formatTimeOnly = (date) => {
    return moment(date).format('HH:mm:ss');
};

export {
    formatDateTime,
    parseDateTime,
    getRelativeTime,
    formatDateOnly,
    formatTimeOnly
};
