const maskLink = /([^"=]{2}|^)((https?|ftp):\/\/\S+[^\s.,> )\];'"!?])/g
const maskMail = /((([0-9A-Za-z]{1}[-0-9A-z.]{1,}[0-9A-Za-z]{1})|([0-9А-Яа-я]{1}[-0-9А-я.]{1,}[0-9А-Яа-я]{1}))@([-A-Za-z]{1,}\.){1,2}[-A-Za-z]{2,})/g

const substLink = '$1<a href="$2" target="_blank">$2</a>'
const substMail = '<a href="mailto:$1" target="_blank">$1</a>'

export const parse = (text: string):string => text
  .replace(maskLink, substLink)
  .replace(maskMail, substMail)
