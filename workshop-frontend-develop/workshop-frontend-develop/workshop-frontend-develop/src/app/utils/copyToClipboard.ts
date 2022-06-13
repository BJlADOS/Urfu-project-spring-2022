function timer(ms: number) {
  return new Promise((resolve) => setTimeout(resolve, ms))
}

export function copyToClipboard(element: HTMLElement, selectionStart: number, selectionEnd?: number) {
  const range = new Range()

  range.setStart(element, selectionStart)
  range.setEnd(element, selectionEnd || element.childNodes.length)
  document.getSelection()?.removeAllRanges()
  document.getSelection()?.addRange(range)
  const isCopied = document.execCommand('copy')

  if (isCopied) {
    timer(100).then(() => {
      document.getSelection()?.removeAllRanges()
    })
  }
}
