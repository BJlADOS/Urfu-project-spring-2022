export function handleDownloadResponse(response: any) {
  const url = window.URL.createObjectURL(new Blob([response.data]))
  const link = document.createElement('a')

  link.href = url
  const contentDisposition = response.headers['content-disposition']
  let fileName = ''

  if (contentDisposition && contentDisposition.indexOf('attachment') !== -1) {
    const filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/
    const matches = filenameRegex.exec(contentDisposition)

    if (matches != null && matches[1]) {
      fileName = matches[1].replace(/['"]/g, '')
    }
  }
  link.setAttribute('download', fileName)
  document.body.appendChild(link)
  link.click()
}
